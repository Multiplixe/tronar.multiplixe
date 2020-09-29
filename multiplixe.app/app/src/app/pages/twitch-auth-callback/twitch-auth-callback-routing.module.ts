import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TwitchAuthCallbackPage } from './twitch-auth-callback.page';

const routes: Routes = [
  {
    path: '',
    component: TwitchAuthCallbackPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TwitchAuthCallbackPageRoutingModule {}
