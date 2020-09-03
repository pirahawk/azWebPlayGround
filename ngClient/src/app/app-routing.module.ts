import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './mycomponents/login.component';

const routes: Routes = [
  { path: '', component: LoginComponent },//TODO Add some other component later otherwise you get 2 app components on same screen
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
