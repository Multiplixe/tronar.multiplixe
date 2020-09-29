import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TwitterAuthCallbackPage } from './twitter-auth-callback.page';

const routes: Routes = [
  {
    path: '',
    component: TwitterAuthCallbackPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TwitterAuthCallbackPageRoutingModule {}
