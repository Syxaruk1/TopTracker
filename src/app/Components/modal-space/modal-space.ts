import { NgTemplateOutlet } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'tt-modal-space',
  imports: [NgTemplateOutlet],
  templateUrl: './modal-space.html',
  styleUrl: './modal-space.css',
})

export class ModalSpace {
  @Output() closeModal = new EventEmitter<void>();
  currentHtmlDiv!: HTMLDivElement;


  onClose()
  {
    this.closeModal.emit();
  }

  choiseTypeSpace(element: HTMLDivElement)
  { 

    if(this.currentHtmlDiv && this.currentHtmlDiv !== element)
    {
      this.currentHtmlDiv.style.border = "1px solid rgba(191,191,191,1)";
    }

    this.currentHtmlDiv = element;
    this.currentHtmlDiv.style.border = "2px solid green";
  }

  selectorSpace = [
    { title: "Личное", description: "мягкий ритм", image: "/Icons/ModalSpace/Plant.png"},
    { title: "Рабочее", description: "фокус и план", image: "/Icons/ModalSpace/Job.png"},
    { title: "Проект", description: "с дедлайнами", image: "/Icons/ModalSpace/Project.png"},
  ]

}
