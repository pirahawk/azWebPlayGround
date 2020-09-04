import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './mycomponents/login.component';
import { UserdisplayComponent } from './mycomponents/userdisplay.component';

const routes: Routes = [
  { path: 'userDisplay', component: UserdisplayComponent },
  { path: '', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
