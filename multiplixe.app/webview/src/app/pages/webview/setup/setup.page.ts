import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { BasePage } from '../../base-page';

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


    this.route.params.subscribe(params => {

      if (!params["socialmedia"]) {
        super.showMessage('rede social não informada na URL, ex: /facebook');
      }

      let url = 'webview/social-media-connection/' + params["socialmedia"];

      this.route.queryParams.subscribe(async queryParams => {


        if (queryParams["access_token"]) {

          this.authService.processAccessToken(queryParams["access_token"])
            .then(() => {
              this.redirect(url);
            })
            .catch(() => {
            });

        }
        else if (queryParams["refresh_token"]) {

          console.log(">>>", url, queryParams["refresh_token"])

          this.authService.processRefreshToken(queryParams["refresh_token"])
            .then(() => {

              console.log("OK")

              this.redirect(url);
            })
            .catch(() => {
            });
        }
        else {
          this.showMessage('parametro access_token não informado')
        }
      });
    });
  }
}
