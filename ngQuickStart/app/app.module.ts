import { NgModule }from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

//import { RouterModule } from '@angular/router'
import { AppRoutingMoudle } from './app-routing.module'
import './rxjs-extensions';

import { InMemoryWebApiModule } from 'angular2-in-memory-web-api';
import { InMemoryDataService } from './in-memory-data.service';

import { AppComponent }from './app.component';
import { HeroesComponent } from './heroes.component';
import { HeroDetailComponent } from './hero-detail.component';
import { DashboardComponent } from './dashboard.component';
import { HeroSearchComponent } from './hero-search.component';

import { HeroService } from './hero.service'

@NgModule({
	imports:[ 
		BrowserModule,
		FormsModule,
		HttpModule,
		InMemoryWebApiModule.forRoot(InMemoryDataService),
		AppRoutingMoudle
		//RouterModule.forRoot([
		//	{
		//		path: '',
		//		redirectTo: '/dashboard',
		//		pathMatch: 'full'
		//	},
		//	{
		//		path: 'heroes',
		//		component: HeroesComponent
		//	},
		//	{
		//		path: 'dashboard',
		//		component: DashboardComponent
		//	},
		//	{
		//		path: 'detail/:id',
		//		component: HeroDetailComponent
		//	}
		//])
	],
  declarations: [ AppComponent,HeroesComponent,HeroDetailComponent,DashboardComponent, HeroSearchComponent ],
  providers:[ HeroService ],
  bootstrap:[ AppComponent ]
})
export class AppModule { }