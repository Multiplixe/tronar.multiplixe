import { Component, OnInit, HostListener } from '@angular/core';
import { Platform } from '@ionic/angular';
import { AppInjector } from 'src/app/app-injector';

@Component({
  selector: 'app-home-screen',
  templateUrl: './home-screen.component.html'
})
export class HomeScreenComponent implements OnInit {

  public show: boolean = false;
  private platform: Platform;
  private deferredPrompt: any;

  public p:string = '';

  constructor() {
    this.platform = AppInjector.get(Platform);
  }

  ngOnInit() {

    this.platform.ready().then(() => {
      if (this.platform.is('desktop')) {
        this.p = 'desktop';
      } else if (this.platform.is('ios')) {
        this.p = 'ios';
      } else if (this.platform.is('android')) {
        this.p = 'android';
      }

      this.show = this.platform.is('android');

    });

    this.show = !window.matchMedia('(display-mode: standalone)').matches;
  }

  @HostListener('window:beforeinstallprompt', ['$event'])
  onbeforeinstallprompt(e) {
    e.preventDefault();
    this.deferredPrompt = e;
  }

  @HostListener('window:appinstalled', ['$event'])
  appinstalled(e) {
    this.show = false;
  }

  addToHomeScreen() {
    this.deferredPrompt.prompt();
    this.deferredPrompt.userChoice
      .then((choiceResult) => {
        if (choiceResult.outcome === 'accepted') {
          console.log('User accepted the A2HS prompt');
        } else {
          console.log('User dismissed the A2HS prompt');
        }
        this.deferredPrompt = null;
        this.show = false;
      });
  }
}
