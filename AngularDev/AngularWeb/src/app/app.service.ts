import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})


export class AppService {

  constructor(private http: HttpClient) { }
   response : any;
  Get(url:string)
   {
    this.http.get(url).subscribe((data) =>
    {
      
      this.response = data;
      
      console.log(data);
    });
    return this.response;
   }

   Post(url:string,param?:any)
   {
       this.http.post(url,param).subscribe((data) =>
       {         
         this.response = data;
         console.log(data);
       });
       return this.response;
   }

}