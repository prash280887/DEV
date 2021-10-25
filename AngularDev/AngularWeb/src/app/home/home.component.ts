import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HomeService } from './home.service';
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  layerdata : any ;
  displayname : string ='';
  routeState : any;
  constructor(private service : HomeService, private router:Router)
  {
   // this.route.params.subscribe( params => console.log(params) );
  
  ​​​​​if(this.router.getCurrentNavigation()?.extras.state)
  {​​​​​this.routeState =this.router.getCurrentNavigation()?.extras.state;
    console.log(this.routeState);
    if(this.routeState)
    {​​​​​;
      let loginUserName = "unknown user" ;
      this.displayname =this.routeState.loginusername ;
      console.log(this.displayname);
    }
  ​​​​​}​​​​​
}​​​​​
  

  public ngOnInit(): void {
 
  }
 

loadData() 
{
  this.layerdata = this.service.LoadData();
}



}
