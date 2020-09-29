import { Component, OnInit, HostListener } from '@angular/core';

import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { RouterRedirectService } from './services/router-redirect.service';
import { AppInjector } from './app-injector';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent implements OnInit {
  public selectedIndex = 0;
  public appPages = [];

  private routerRedirectService: RouterRedirectService;

  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar
  ) {
    this.initializeApp();

    this.routerRedirectService = AppInjector.get(RouterRedirectService)

  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  ngOnInit() {

    let pathRedirectToLogin = [
      '/redefinir-senha',
      '/esqueci-minha-senha',
      '/r/twitter-auth-callback',
      '/r/twitch-auth-callback',
      '/r/youtube-auth-callback',
      '/politica-de-privacidade',
      '/termos-de-uso'
    ];

    let currentPath = this.routerRedirectService.getCurrent();

    let splitCurrentPath = currentPath.split('?')

    if (pathRedirectToLogin.indexOf(splitCurrentPath[0]) == -1) {
      this.routerRedirectService.redirect('login');
    }
  }
}
