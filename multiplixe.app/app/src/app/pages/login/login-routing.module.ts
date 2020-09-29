import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ComponentModule } from 'src/app/components/component.module';

import { LoginPage } from "./login.page";

const routes: Routes = [
  {
    path: '',
    component: LoginPage
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    ComponentModule,
  ],
  exports: [RouterModule],
})
export class LoginPageRoutingModule {}
