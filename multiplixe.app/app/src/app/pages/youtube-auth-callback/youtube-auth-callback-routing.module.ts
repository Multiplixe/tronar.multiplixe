import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { YoutubeAuthCallbackPage } from './youtube-auth-callback.page';

const routes: Routes = [
  {
    path: '',
    component: YoutubeAuthCallbackPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class YoutubeAuthCallbackPageRoutingModule {}
