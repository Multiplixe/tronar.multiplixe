import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SetupPage } from './pages/setup/setup.page';
import { TwitterCallbackPage } from './pages/social-media-callback/twitter-callback.page';
import { SocialMediaConnectionPage } from './pages/social-media-connection/social-media-connection.page';

const routes: Routes = [
  {
    path: 'setup/:socialmedia',
    component: SetupPage
  },
  {
    path: 'social-media-connection/:socialmedia',
    component: SocialMediaConnectionPage
  },
  {
    path: 'twitter-callback',
    component: TwitterCallbackPage
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
