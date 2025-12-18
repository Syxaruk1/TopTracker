import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { email } from '@angular/forms/signals';

@Injectable({
  providedIn: 'root',
})

export class BackendProvider {
  constructor(private httpClient: HttpClient){};
  
 
  register(userName: string, email: string, password: string)
  {
    this.httpClient.post<any>('https://localhost:7124/account/register/',
    {
      "UserName": userName,
      "Email": email,
      "Password": password
    },
    {
      withCredentials: true 
    }).subscribe((dd: HttpResponse<any>) => {
      console.log('Был выполнен запрос на добавление задачи');
      
    }); 

  }

  addTask()
  {
    this.httpClient.post<any>('https://localhost:7124/task?title=Wrrre&description=desc',{}).subscribe((next:any)=>{
      console.log('Был выполнен запрос на добавление задачи');
    });
  }

}
