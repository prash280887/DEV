import {AppService} from '../app.service'
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class HomeService
{
layerdata : any;
status : any ;

constructor(private service: AppService)
{
}


LoadData() 
{                 
 // this.layerdata =  this.service.Get('api/homeapi/getdata/1') ;
 this.layerdata =  this.service.Get('api/user/getallusers') ;      
       
    return this.layerdata; 
}

}