import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { concatMap, every, throwError, timeInterval } from 'rxjs';
import * as Three from 'three';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';
import { OrbitControls, OutlineEffect } from 'three/examples/jsm/Addons.js';
import { Sky } from 'three/addons/objects/Sky.js';
import Stats from 'three/examples/jsm/libs/stats.module.js';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App implements OnInit {
  
  ngOnInit(): void {
    this.createThreeJsBox();
  }




  createThreeJsBox(): void{

    // RENDER
    const render = new Three.WebGLRenderer({antialias: true, alpha: true}); 
    render.setSize(window.innerWidth, window.innerHeight);
    render.toneMapping = Three.ACESFilmicToneMapping;
    render.toneMappingExposure = 0.5;
    document.body.appendChild(render.domElement);
    
    // SCENE
    const scene = new Three.Scene();

    // CAMERA
    const camera = new Three.PerspectiveCamera(
      75,
      window.innerWidth / window.innerHeight, 
      0.1, 
      1000
    );
    const controls = new OrbitControls(camera, render.domElement);
    controls.enableZoom = true;
    controls.enablePan = true;
    controls.enableDamping = true;
    camera.position.set(1, 0.5, 2);
    camera.name = "camera";
    

    
    // LIGHT
    const directionLight = new Three.DirectionalLight();
    directionLight.castShadow = true;
    directionLight.intensity = 7;
    const sky = new Sky();
    sky.scale.setScalar(45000);
    sky.name = "sky";
    scene.add(sky);

    const sun = new Three.Vector3();
    const uniforms = sky.material.uniforms;
    uniforms['turbidity'].value = 10;
    uniforms['rayleigh'].value = 0.1;    
    uniforms['mieCoefficient'].value = 0.002;  
    uniforms['mieDirectionalG'].value = 0,763; 
    const phi = Three.MathUtils.degToRad(90 - 30);
    const theta = Three.MathUtils.degToRad(90); 
    sun.setFromSphericalCoords(1, phi, theta);
    uniforms['sunPosition'].value.copy(sun);
    directionLight.position.copy(sun).normalize().multiplyScalar(100);
    directionLight.name ="light";
    scene.add(directionLight);



    // MESHES
    // Materials
    const textureLoader = new Three.TextureLoader();
 
    const textures = {
      map: textureLoader.load('/Tree/Texture/bark_willow_02_diff_1k.jpg'),      
      aoMap: textureLoader.load('/Tree/Texture/bark_willow_02_ao_1k.jpg'),              
      roughnessMap: textureLoader.load('/Tree/Texture/bark_willow_02_arm_1k.jpg'),    
      displacementMap: textureLoader.load('/Tree/Texture/bark_willow_02_disp_1k.png'),  
      normalMap: textureLoader.load('/Tree/Texture/bark_willow_02_nor_gl_1k.jpg')           
    };
    Object.values(textures).forEach(tex => {
      tex.anisotropy = 16;
      tex.colorSpace = Three.SRGBColorSpace; 
    });

    // LOADERS
    const loader = new GLTFLoader();

    //#region
    loader.load("Plane.glb",(gltf)=>{
      gltf.scene.position.y = -0.8;
      gltf.scene.scale.setScalar(0.1);
      scene.add(gltf.scene);
      
    })
   
    const raycaster = new Three.Raycaster();
    raycaster.layers.set(1);
  
    render.domElement.addEventListener('pointerdown', (event) => Ray("onRayClick",event));
    render.domElement.addEventListener('pointermove', (event) => Ray("onRayHover",event));


    //#region Рабочий код
    // Load моделька дерева
    loader.load("/Tree/BaseTree.glb",(gltf)=>{
      const model = gltf.scene;
      model.name = "Tree";
      model.position.y = -0.8;

      model.traverse((children)=>{
        if(children instanceof Three.Mesh)
        {
          children.material = new Three.MeshStandardMaterial({
            map: textures.map,
            aoMap: textures.aoMap,
            aoMapIntensity: 1,
            roughnessMap: textures.roughnessMap,
            displacementMap: textures.displacementMap,
            displacementScale: 0.05,
            normalMap: textures.normalMap,
            normalScale: new Three.Vector2(2,2)
          }); 
          children.material.needsUpdate = true;
        }
      })
      
      model.scale.set(0.2,0.2,0.2);
      model.updateMatrixWorld(); 
      scene.add(model);
    })

    const allBranch = [
      "branch_0",
      "branch_1",
      "branch_2",
      "branch_3",
    ];

    const random = allBranch[Math.floor(Math.random() * allBranch.length)];
    // Load Ветка для дерева - проект
    loader.load(`/Tree/Branches_base_project/${random}.glb`,(gltf)=>
    {
      console.log(gltf.scene);
      const model = gltf.scene;
      model.scale.set(0.2,0.2,0.2);
      model.traverse((children)=>{
        if(children instanceof Three.Mesh)
        {
          children.material = new Three.MeshStandardMaterial({
            map: textures.map,
            aoMap: textures.aoMap,
            aoMapIntensity: 1,
            roughnessMap: textures.roughnessMap,
            displacementMap: textures.displacementMap,
            displacementScale: 0.05,
            normalMap: textures.normalMap,
            normalScale: new Three.Vector2(2,2)
          }); 
          children.material.needsUpdate = true;
        }
      })
      const tree = scene.getObjectByName("Tree");
      
      const ChoiceBranch = tree?.children.filter(el => el.name !== "tree")!;
      const currentBranch = ChoiceBranch[Math.floor(Math.random() * ChoiceBranch?.length)] as Three.Mesh;
      
      const positions = currentBranch.geometry.getAttribute("position").clone();
      positions.applyMatrix4(currentBranch.matrixWorld);

      const randomIndex = Math.floor(Math.random() * positions.count);
      const randomVector = new Three.Vector3(positions.getX(randomIndex),positions.getY(randomIndex),positions.getZ(randomIndex));

      model.position.copy(randomVector);
      model.updateMatrixWorld();
      scene.add(model);    
    });

    const allBranchesUnder = [
      "branch_under_0",
      "branch_under_1",
      "branch_under_2",
      "branch_under_3",
      "branch_under_4",
      "branch_under_5",
    ];

    // Load ветка для ветки - под-проект
    const randomUnder = allBranchesUnder[Math.floor(Math.random() * allBranchesUnder.length)];
    loader.load(`/Tree/Branches_under_project/${randomUnder}.glb`,(gltf)=>{
      console.log(gltf.scene);
      const model = gltf.scene;
      model.scale.set(0.2,0.2,0.2);
      model.traverse((children)=>{
        if(children instanceof Three.Mesh)
        {
          children.material = new Three.MeshStandardMaterial({
            map: textures.map,
            aoMap: textures.aoMap,
            aoMapIntensity: 1,
            roughnessMap: textures.roughnessMap,
            displacementMap: textures.displacementMap,
            displacementScale: 0.05,
            normalMap: textures.normalMap,
            normalScale: new Three.Vector2(2,2)
          }); 
          children.material.needsUpdate = true;
        }
      })
      const branch = scene.getObjectByName(random) as Three.Mesh;

      const pointsBranch = branch.geometry.getAttribute("position").clone();
      pointsBranch.applyMatrix4(branch.matrixWorld);

      const randomIndex = Math.floor(Math.random() * pointsBranch.count);
      const randomVector = new Three.Vector3(pointsBranch.getX(randomIndex),pointsBranch.getY(randomIndex),pointsBranch.getZ(randomIndex));

      model.position.copy(randomVector);
      model.updateMatrixWorld();
      scene.add(model);  

    })

    // Load листья для ветки - любой
    loader.load("/Tree/Leaves.glb",(gltf)=>{
      const model = gltf.scene;
      const branch = scene.getObjectByName(randomUnder) as Three.Mesh;
      branch.geometry.computeVertexNormals();
      const points = branch.geometry.getAttribute("position");
      const normals = branch.geometry.getAttribute("normal");
  
      for(let i = 0; i < 200; i++)
      {
        const modelCopy = model.clone();
        const randomIndex = Math.floor(Math.random() * points.count);
        const position = new Three.Vector3()
          .fromBufferAttribute(points, randomIndex);
        const normal = new Three.Vector3()
        .fromBufferAttribute(normals, randomIndex)
        .normalize();
      
        position.applyMatrix4(branch.matrixWorld);
        normal.transformDirection(branch.matrixWorld);
       
        modelCopy.position.copy(position);
        modelCopy.up.copy(normal);
        modelCopy.lookAt(position.clone().add(normal));
        modelCopy.scale.set(0.025,0.025,0.025);
        modelCopy.rotation.x -= 0.1 + Math.random() * 0.2;
        modelCopy.rotation.y += Math.random() * Math.PI * 0.5;
   
        modelCopy.updateMatrixWorld();
        scene.add(modelCopy);  
      }
    
    })
    //#endregion

    // FUNCTION
    function animate() {
      requestAnimationFrame(animate);
      controls.update(); 
      render.render(scene, camera);
		};

    function Ray(callback: string, event: PointerEvent)
    {
      const coords = new Three.Vector2
      (
        (event.clientX / render.domElement.clientWidth ) * 2 - 1,
        -(event.clientY / render.domElement.clientHeight ) * 2 + 1
      )

      raycaster.setFromCamera(coords,camera);

      const near = camera.near || 0.1;
      raycaster.ray.origin.addScaledVector(raycaster.ray.direction, near);

      const intersections = raycaster.intersectObjects(scene.children,true);

      if(intersections.length > 0)
      {      
        const currentObject = intersections[0].object;

        if(currentObject.userData[callback])
        {
          intersections[0].object.userData[callback]();
        }
      }

    }
    

    // EVENTS
    window.addEventListener('resize', () => {
        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();
        render.setSize(window.innerWidth, window.innerHeight);
    });

    // CALL FUNCTION
    animate();
  }
  
}
