import { Component, OnInit } from '@angular/core';
import { BasePage } from '../base-page';
// import * as texts from '../../../assets/json/social-media-connection.json';
import { AppInjector } from 'src/app/app-injector';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-social-media-connection',
  templateUrl: './social-media-connection.page.html'
})
export class SocialMediaConnectionPage extends BasePage implements OnInit {

  private socialMedia: string = '';
  public activatedRoute: ActivatedRoute;

  constructor() {
    super();
    this.activatedRoute = AppInjector.get(ActivatedRoute);
  }

  ngOnInit() {
 
    console.log('>', this.activatedRoute.snapshot.paramMap.get("socialmedia"));
    console.log('>', this.activatedRoute.snapshot.params.socialmedia);
    console.log('>', this.activatedRoute.snapshot.data);

    this.activatedRoute.queryParams.subscribe(params => {
      console.log('>', params)
    });

    this.activatedRoute.params.subscribe(params => {
      console.log('>', params)
    });

  }

}
