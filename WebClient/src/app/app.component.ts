
import { Component } from '@angular/core';
import { map } from 'rxjs';
import { MethodService } from 'src/methodservices/method.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WebClient';
  constructor(    private method: MethodService
  ) {
   
  }
  saved() {
    this.method.Post('api/User',{name:'hello1'}).subscribe((data: any)=>{
      debugger
      console.log(data);
      //this.products = data;
    }) 
    // this.method.Get('api/User').subscribe((data: any)=>{
    //   debugger
    //   console.log(data);
    //   //this.products = data;
    // }) 
   
  }
}
