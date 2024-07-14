import { HttpClient } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FlexiGridModule, StateModel } from 'flexi-grid';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FlexiGridModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  data = signal<any[]>([]);

  state = signal<StateModel>(new StateModel());
  constructor(
    private http: HttpClient
  ){
    this.getAll();
  }

  getAll(){
    let endpoint = "https://localhost:7124/api/Todos/GetAll";

     this.http.get(endpoint)
       .subscribe((res:any)=> {
        this.data.set(res);
       });
  }

  onStateChange(event: StateModel){
    this.state.set(event);
    this.getAll();
  }
}
