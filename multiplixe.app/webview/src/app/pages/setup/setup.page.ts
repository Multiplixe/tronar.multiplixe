import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { BasePage } from '../base-page';

@Component({
  selector: 'app-setup-page',
  templateUrl: './setup.page.html'
})
export class SetupPage extends BasePage implements OnInit {

  private route: ActivatedRoute;
  public authService: AuthService;

  constructor(injector: Injector) {
    super();
    this.route = injector.get(ActivatedRoute);
    this.authService = injector.get(AuthService);
  }

  ngOnInit(): void {

    this.route.queryParams.subscribe(async params => {
      console.log(">>>", params)

      this.authService.processToken(params["access_token"])

      this.route.params.subscribe(params => {
        this.redirect('social-media-connection/' + params["socialmedia"]);
      });

    });

  }

}
