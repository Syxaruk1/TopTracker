import { Component, Input } from '@angular/core';
import { TreeViewer } from './Components/tree-viewer/tree-viewer';
import { SideBar } from './Components/side-bar/side-bar';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { ModalRegister } from './Components/modal-register/modal-register';


@Component({
  selector: 'app-root',
  imports: [TreeViewer,SideBar,ModalRegister],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {  


}


