import { Component, signal } from '@angular/core';
import { ModalSpace } from '../modal-space/modal-space';

@Component({
  selector: 'tt-side-bar',
  imports: [ModalSpace],
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.css',
})


export class SideBar {

  isShowSpan = false;
  isShowModalSpace = false;

  hideSpan()
  {
    this.isShowSpan = !this.isShowSpan;
  }

  showSpace()
  {
    this.isShowModalSpace = !this.isShowModalSpace;
  }
}
