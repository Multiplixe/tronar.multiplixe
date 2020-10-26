import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path : '',
    redirectTo: 'social-media-connection/234234',
    pathMatch: 'full'
  },
  {
    path: 'social-media-connection/:socialmedia',
    loadChildren: () => import('./pages/social-media-connection/social-media-connection.module').then( m => m.SocialMediaConnectionPageModule)
  } 
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
