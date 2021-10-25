import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from './login.service';
import { LoginUser } from './loginuser.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
response : any;
status : any ;
login : LoginUser = new LoginUser();

  constructor(private service: LoginService ,private router:Router)
  {
    this.login.UserName = "";
    this.login.Password = "";
  }

  public ngOnInit(): void {
    
  }

  title = 'AngularWeb';

submit()
{
 this.response = this.service.AuthenticateUser(this.login);
if(this.response.status == "Success")
{
//this.router.navigateByUrl("/home",);
this.router.navigate(['home'],{ state : { loginusername: this.response.username } });

} 
  
}

}
