import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { AppInjector } from '../app-injector';
import { Router, ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class RouterRedirectService extends BaseService {

  private navCtrl: NavController;
  private router: Router;
  private location: Location;
  private activatedRoute : ActivatedRoute ;

  constructor() {
    super()
    this.navCtrl = AppInjector.get(NavController)
    this.router = AppInjector.get(Router)
    this.location = AppInjector.get(Location)
    this.activatedRoute = AppInjector.get(ActivatedRoute)
  }

  getParameterFromHash() {

    let params = this.activatedRoute.snapshot.fragment;

    return JSON.parse(
        '{"' +
            decodeURI(params)
                .replace(/"/g, '\\"')
                .replace(/&/g, '","')
                .replace(/=/g, '":"') +
            '"}'
    );
    
  }

  getCurrent(includeHash = false) {
    return this.location.path(includeHash);
  }

  redirect(route: string) {
    this.navCtrl.navigateRoot(route);
  }

  navigateForward(route: string) {
    this.navCtrl.navigateForward(route);
  }  

  redirectRestrict(route: string) {
    this.redirect('r/' + route);
  }

  navigateForwardRestrict(route: string) {
    this.navigateForward('r/' + route);
  }  

}
 