import { Component } from '@angular/core';
import { TreeViewer } from './Components/tree-viewer/tree-viewer';
import { SideBar } from './Components/side-bar/side-bar';

@Component({
  selector: 'app-root',
  imports: [TreeViewer,SideBar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  
}
