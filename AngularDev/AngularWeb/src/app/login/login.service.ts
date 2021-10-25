import {AppService} from '../app.service'
import { LoginUser } from './loginuser.model';
import { Injectable } from '@angular/core';
import { createElementCssSelector } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})

export class LoginService
{
status : any ;
username : any;
constructor(private service: AppService)
{
}

AuthenticateUser(user:LoginUser)
{         
     // this.status =  this.service.Post('api/homeapi/Authenticate',user);
      var response = this.service.Post('api/user/authenticate',user);
      this.status = response.loginuser.Status;
      if(this.status == "Success")
      {
      this.username = response.userdetail.name;
      return  { status : this.status , username : this.username  }
      }
      
      {
        return  { status : this.status , username : ""  }
      }
}


}