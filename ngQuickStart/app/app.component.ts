import { Component } from '@angular/core';


@Component({
	selector: 'my-app',
  	template:`
  		<h1>{{title}}</h1>
  		<nav>
	  		<a routerLink='/heroes' routerLinkActive="active">Heroes</a>
	  		<a routerLink='/dashboard' routerLinkActive="active">dashboard</a>
  		</nav>
  		<router-outlet></router-outlet>
  	`,
  	styleUrls:['./styles/app.component.css']
})
export class AppComponent { 
	title = 'Tour of Heros';
	
}

