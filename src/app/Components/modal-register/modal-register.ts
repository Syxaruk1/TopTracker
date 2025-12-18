import { Component, ElementRef, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BackendProvider } from '../../Servcices/backend-provider';

@Component({
  selector: 'app-modal-register',
  imports: [FormsModule],
  templateUrl: 'modal-register.html',
  styleUrl: 'modal-register.css',
})

export class ModalRegister {
  email: string = "";
  password: string = "";
  @ViewChild("Register") modal!: ElementRef<HTMLDivElement>;;

  constructor(private service: BackendProvider){}

  clickHandle()
  {
    this.service.register("testFFF",this.email,this.password);
    this.modal.nativeElement.style.display = "none";
  }
}
