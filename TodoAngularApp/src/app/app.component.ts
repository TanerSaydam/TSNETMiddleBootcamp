import { HttpClient } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FlexiGridModule, FlexiGridService, StateModel } from 'flexi-grid';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FlexiGridModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  data = signal<any[]>([]);
  total = signal<number>(0);

  state = signal<StateModel>(new StateModel());
  constructor(
    private http: HttpClient,
    private flexi: FlexiGridService
  ){
    this.getAll();
  }

  getAll(){
    let endpoint = "https://localhost:7124/api/Todos/GetAll?count=true&";
    let stateResponse =  this.flexi.getODataEndpoint(this.state());
    endpoint += stateResponse;    
    
     this.http.get(endpoint)
       .subscribe((res:any)=> {
        this.data.set(res.data);
        this.total.set(res.total);
       });
  }

  onStateChange(event: StateModel){
    this.state.set(event);
    this.getAll();
  }
}
