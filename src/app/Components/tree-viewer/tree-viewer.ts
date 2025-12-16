import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import * as Three from 'three';
import { GLTFLoader, OrbitControls } from 'three/examples/jsm/Addons.js';
import { Sky } from 'three/examples/jsm/objects/Sky.js';

@Component({
  selector: 'app-tree-viewer',
  imports: [],
  standalone: true,
  templateUrl: './tree-viewer.html',
  styleUrl: './tree-viewer.css',
})

export class TreeViewer implements AfterViewInit, OnDestroy{

  @ViewChild('canvasContainer') 
  private canvasContainer!: ElementRef;
  private animationId: number = 0;
  private render!: Three.WebGLRenderer;
  private scene;
  private camera;
  private controls!: OrbitControls;
  private sky!: Sky;
  private textureLoader;
  private modelLoader;
  private textureWood: TextureWood;
  private raycaster;

  constructor() {
    this.scene = new Three.Scene();
    this.camera = new Three.PerspectiveCamera(
      75, 
      window.innerWidth / window.innerHeight,
      0.1,
      1000
    )

    this.textureLoader = new Three.TextureLoader();
    this.modelLoader = new GLTFLoader();
    this.textureWood = {
      map: null, 
      aoMap: null, 
      roughnessMap: null,
      displacementMap: null, 
      normalMap: null
    };
    this.raycaster = new Three.Raycaster();
  }
  ngOnDestroy(): void {
    cancelAnimationFrame(this.animationId);
  }

  ngAfterViewInit(): void {
    this.configRender();
    this.configCamera();
    this.controls = new OrbitControls(this.camera, this.render.domElement);
    this.sky = new Sky();
    
    this.configControls();
    this.configLight();
    this.loadingModelPlane();
    this.loadingModelThree()
    this.configHandler();
    this.configRaycaster()
    this.loadingTextureForWood();
    this.animate();
  }

  applyWoodMaterial(mesh: Three.Mesh): void {

    mesh.material = new Three.MeshStandardMaterial({
      map: this.textureWood.map!,
      aoMap: this.textureWood.aoMap!,
      aoMapIntensity: 1,
      roughnessMap: this.textureWood.roughnessMap!,
      displacementMap: this.textureWood.displacementMap!,
      displacementScale: 0.05,
      normalMap: this.textureWood.normalMap!,
      normalScale: new Three.Vector2(2, 2)
    });
  (mesh.material as Three.MeshStandardMaterial).needsUpdate = true;
 }

  loadingModelBranch()
  {
    const random = allBranch[Math.floor(Math.random() * allBranch.length)];
    this.modelLoader.load(`/Tree/Branches_base_project/${random}.glb`,(gltf)=>
    {
      console.log(gltf.scene);
      const model = gltf.scene;
      model.scale.set(0.2,0.2,0.2);
      model.traverse((children)=>{
        if(children instanceof Three.Mesh)
        {
          this.applyWoodMaterial(children);
        }
      })

      const tree = this.scene.getObjectByName("Tree");
      
      const ChoiceBranch = tree?.children.filter(el => el.name !== "tree")!;
      const currentBranch = ChoiceBranch[Math.floor(Math.random() * ChoiceBranch?.length)] as Three.Mesh;
      
      const positions = currentBranch.geometry.getAttribute("position").clone();
      positions.applyMatrix4(currentBranch.matrixWorld);

      const randomIndex = Math.floor(Math.random() * positions.count);
      const randomVector = new Three.Vector3(positions.getX(randomIndex),positions.getY(randomIndex),positions.getZ(randomIndex));

      model.position.copy(randomVector);
      model.updateMatrixWorld();
      this.scene.add(model);   

    });
  }

  loadingModelUnderBranch(nameBranch: string)
  {
    const randomUnder = allBranchesUnder[Math.floor(Math.random() * allBranchesUnder.length)];
    this.modelLoader.load(`/Tree/Branches_under_project/${randomUnder}.glb`,(gltf)=>
    {
      console.log(gltf.scene);
      const model = gltf.scene;
      model.scale.set(0.2,0.2,0.2);
      model.traverse((children)=>{
        if(children instanceof Three.Mesh)
        {
          this.applyWoodMaterial(children);
        }
      })
      const branch = this.scene.getObjectByName(nameBranch) as Three.Mesh;
    
      const pointsBranch = branch.geometry.getAttribute("position").clone();
      pointsBranch.applyMatrix4(branch.matrixWorld);
    
      const randomIndex = Math.floor(Math.random() * pointsBranch.count);
      const randomVector = new Three.Vector3(pointsBranch.getX(randomIndex),pointsBranch.getY(randomIndex),pointsBranch.getZ(randomIndex));

      model.position.copy(randomVector);
      model.updateMatrixWorld();
      this.scene.add(model);  
    
    })
  }

  loadingModelLeaves(nameBranch: string)
  {
    this.modelLoader.load("/Tree/Leaves.glb",(gltf)=>
    {
      const model = gltf.scene;
      const branch = this.scene.getObjectByName(nameBranch) as Three.Mesh;
      branch.geometry.computeVertexNormals();

      const points = branch.geometry.getAttribute("position");
      const normals = branch.geometry.getAttribute("normal");

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
      this.scene.add(modelCopy);       
    })
  }
  loadingModelThree()
  {
    this.modelLoader.load("/Tree/BaseTree.glb",(gltf)=>
    {
      const model = gltf.scene;
      model.name = "Tree";
      model.position.y = -0.8;
    
      model.traverse((children)=>
      {
        if(children instanceof Three.Mesh)
        {
          this.applyWoodMaterial(children);
        }
      })
          
      model.scale.set(0.2,0.2,0.2);
      model.updateMatrixWorld(); 
      this.scene.add(model);
    })
  }

  loadingModelPlane()
  {
    this.modelLoader.load("Plane.glb",(gltf)=>{
      gltf.scene.position.y = -0.8;
      gltf.scene.scale.setScalar(0.1);
      this.scene.add(gltf.scene);     
    })
  }

  configHandler()
  {
    this.render.domElement.addEventListener('pointerdown', (event) => this.Ray("onRayClick",event));
    this.render.domElement.addEventListener('pointermove', (event) => this.Ray("onRayHover",event));
    window.addEventListener('resize', () => {
      this.camera.aspect = window.innerWidth / window.innerHeight;
      this.camera.updateProjectionMatrix();
      this.render.setSize(window.innerWidth, window.innerHeight);
    });
  }

  loadingTextureForWood()
  {  
    this.textureWood = {
      map: this.textureLoader.load('/Tree/Texture/bark_willow_02_diff_1k.jpg'),      
      aoMap: this.textureLoader.load('/Tree/Texture/bark_willow_02_ao_1k.jpg'),              
      roughnessMap: this.textureLoader.load('/Tree/Texture/bark_willow_02_arm_1k.jpg'),    
      displacementMap: this.textureLoader.load('/Tree/Texture/bark_willow_02_disp_1k.png'),  
      normalMap: this.textureLoader.load('/Tree/Texture/bark_willow_02_nor_gl_1k.jpg')           
    };
    
    // настройка каждому материалу 
    (Object.values(this.textureWood) as Three.Texture[]).forEach(tex => {
      tex.anisotropy = 16;
      tex.colorSpace = Three.SRGBColorSpace; 
      tex.needsUpdate = true;
    });
  }

  configLight()
  {
    // Настройка света
    const sun = new Three.Vector3();
    const directionLight = new Three.DirectionalLight();
    directionLight.castShadow = true;
    directionLight.intensity = 7;

    // Настройка небо
    this.sky.scale.setScalar(45000);
    this.scene.add(this.sky);
    const uniforms = this.sky.material.uniforms;
    uniforms['turbidity'].value = 10;
    uniforms['rayleigh'].value = 0.1;    
    uniforms['mieCoefficient'].value = 0.002;  
    uniforms['mieDirectionalG'].value = 0,763; 
    const phi = Three.MathUtils.degToRad(90 - 30);
    const theta = Three.MathUtils.degToRad(90); 
    sun.setFromSphericalCoords(1, phi, theta);
    uniforms['sunPosition'].value.copy(sun);
    directionLight.position.copy(sun).normalize().multiplyScalar(100);
    this.scene.add(directionLight);
  }

  configCamera()
  {
    this.camera.position.set(1, 0.5, 2);
  }

  configControls()
  {
    this.controls.enableZoom = true;
    this.controls.enablePan = true;
    this.controls.enableDamping = true;
  }

  configRaycaster()
  {
    this.raycaster.ray.origin.addScaledVector(this.raycaster.ray.direction, this.camera.near || 0.1);
  }

  configRender()
  {
    this.render = new Three.WebGLRenderer({
      antialias: true, 
      alpha: true
    });
    this.render.setSize(window.innerWidth, window.innerHeight);
    this.render.toneMapping = Three.ACESFilmicToneMapping;
    this.render.toneMappingExposure = 0.5;
    this.canvasContainer.nativeElement.appendChild(this.render.domElement);
  }

  animate = () =>
  {
      this.animationId = requestAnimationFrame(this.animate);
      this.controls.update(); 
      this.render.render(this.scene, this.camera);
	};

  Ray(callback: string, event: PointerEvent)
  {
    const coords = new Three.Vector2
    (
      (event.clientX / this.render.domElement.clientWidth ) * 2 - 1,
      -(event.clientY / this.render.domElement.clientHeight ) * 2 + 1
    )
  
    this.raycaster.setFromCamera(coords,this.camera);
  
    const intersections = this.raycaster.intersectObjects(this.scene.children,true);
  
    if(intersections.length > 0)
    {      
      const currentObject = intersections[0].object;
      if(currentObject.userData[callback])
      {
        intersections[0].object.userData[callback]();
      }
    }
  }
}

const allBranch = [
  "branch_0",
  "branch_1",
  "branch_2",
  "branch_3",
];

const allBranchesUnder = [
  "branch_under_0",
  "branch_under_1",
  "branch_under_2",
  "branch_under_3",
  "branch_under_4",
  "branch_under_5",
];

interface TextureWood{
  map: Three.Texture | null;
  aoMap: Three.Texture | null;
  roughnessMap: Three.Texture | null;
  displacementMap: Three.Texture | null;
  normalMap: Three.Texture | null;
}
