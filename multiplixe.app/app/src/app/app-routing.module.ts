import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AppRestrictedComponent } from './app.restricted.component';
import { AuthGuard } from './guards/auth.guard';
import { AppSetupComponent } from './app.setup.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'r',
    redirectTo: 'r/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'cadastro',
    loadChildren: () => import('./pages/register/register.module').then(m => m.RegisterPageModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./pages/login/login.module').then(m => m.LoginPageModule)
  },
  {
    path: "setup",
    component: AppSetupComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "r",
    component: AppRestrictedComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: "dashboard",
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/dashboard/dashboard.module').then(m => m.DashboardPageModule)
      },
      {
        path: "minhas-conexoes",
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/profiles-connection/profiles-connection.module').then(m => m.ProfilesConnectionPageModule)
      },
      {
        path: 'twitter-auth-callback',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/twitter-auth-callback/twitter-auth-callback.module').then(m => m.TwitterAuthCallbackPageModule)
      },
      {
        path: 'twitch-auth-callback',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/twitch-auth-callback/twitch-auth-callback.module').then(m => m.TwitchAuthCallbackPageModule)
      },
      {
        path: 'youtube-auth-callback',
        loadChildren: () => import('./pages/youtube-auth-callback/youtube-auth-callback.module').then( m => m.YoutubeAuthCallbackPageModule)
      },      
      {
        path: 'versionamento',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/version/version.module').then(m => m.VersionPageModule)
      },
      {
        path: 'perfil',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/user-profile/user-profile-edit/user-profile-edit.module').then(m => m.UserProfileEditPageModule)
      },
      {
        path: 'perfil/avatar',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/user-profile/user-profile-upload-avatar/user-profile-upload-avatar.module').then(m => m.UserProfileUploadAvatarModule)
      },
      {
        path: 'ranking',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/ranking/ranking.module').then(m => m.RankingPageModule)
      },
      {
        path: 'minhas-conexoes/facebook',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/social-media-profile-dashboard/facebook/facebook.module').then(m => m.FacebookPageModule)
      },
      {
        path: 'minhas-conexoes/twitter',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/social-media-profile-dashboard/twitter/twitter.module').then(m => m.TwitterPageModule)
      },
      {
        path: 'minhas-conexoes/twitch',
        canActivate: [AuthGuard],
        loadChildren: () => import('./pages/social-media-profile-dashboard/twitch/twitch.module').then(m => m.TwitchPageModule)
      },
      {
        path: 'minhas-conexoes/youtube',
        loadChildren: () => import('./pages/social-media-profile-dashboard/youtube/youtube.module').then( m => m.YoutubePageModule)
      }      
    ]
  },
  {
    path: 'sobre',
    loadChildren: () => import('./pages/about/about.module').then(m => m.AboutPageModule)
  },
  {
    path: 'redefinir-senha',
    loadChildren: () => import('./pages/password-reset/password-reset.module').then(m => m.PasswordResetPageModule)
  },
  {
    path: 'esqueci-minha-senha',
    loadChildren: () => import('./pages/password-reset-request/password-reset-request.module').then(m => m.PasswordResetRequestPageModule)
  },
  {
    path: 'politica-de-privacidade',
    loadChildren: () => import('./pages/privacy/privacy.module').then( m => m.PrivacyPageModule)
  },
  {
    path: 'termos-de-uso',
    loadChildren: () => import('./pages/terms-of-use/terms-of-use.module').then( m => m.TermsOfUsePageModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
