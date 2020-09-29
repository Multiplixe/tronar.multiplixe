import { Platform } from '@ionic/angular';
import { OnInit, AfterViewInit, Component, NgZone } from '@angular/core';
import { AppInjector } from './app-injector';
import { RouterRedirectService } from './services/router-redirect.service';
import { AuthService } from './services/auth.service';
import { FirebaseListener } from './services/activities/firebase-listener';
import { FirebaseListener as FirebaseListenerCommon } from './services/activities-common/firebase-listener';
import { PushNotificationService } from './services/push-notification.service';
import { AngularFireAuth } from '@angular/fire/auth';
import { EventEmitterService } from './services/event-emitter.service';
import { ActivitiesEnum } from './enums/activities.enum';
import { DashboardService } from './services/dashboard.service';
import { Dashboard as DashboardDto } from './dtos/dashboard';
import { DisplayControl } from './dtos/display-control';
import { RankingService } from './services/ranking.service';
import { TokenAuth } from './dtos/token-auth';

@Component({
  selector: 'app-restricted',
  templateUrl: 'app.restricted.component.html'
})
export class AppRestrictedComponent implements OnInit, AfterViewInit {

  public appPages = [
    {
      title: 'Início',
      url: '/r/dashboard',
      icon: 'paper-plane'
    },
    {
      title: 'Minhas conexões',
      url: '/r/minhas-conexoes',
      icon: 'paper-plane'
    },
    {
      title: 'Ranking',
      url: '/r/ranking',
      icon: 'paper-plane'
    }
    , {
      title: 'Perfil',
      url: '/r/perfil',
      icon: 'paper-plane'
    }
  ];

  public nickname: string = '';
  public ranking: number = 0;
  public dashboardDto: DashboardDto = DashboardDto.create();

  private authService: AuthService;
  private routerRedirectService: RouterRedirectService;
  private firebaseListener: FirebaseListener;
  private firebaseListenerCommon: FirebaseListenerCommon;
  private pushNotificationService: PushNotificationService;
  private angularFireAuth: AngularFireAuth;
  protected eventEmitter: EventEmitterService;
  private dashboardService: DashboardService;
  private rankingService: RankingService;

  public displayControl: DisplayControl = DisplayControl.create();

  constructor(private platform: Platform) {
    this.authService = AppInjector.get(AuthService);
    this.routerRedirectService = AppInjector.get(RouterRedirectService);
    this.firebaseListener = AppInjector.get(FirebaseListener);
    this.pushNotificationService = AppInjector.get(PushNotificationService);
    this.angularFireAuth = AppInjector.get(AngularFireAuth);
    this.eventEmitter = AppInjector.get(EventEmitterService);
    this.dashboardService = AppInjector.get(DashboardService);
    this.rankingService = AppInjector.get(RankingService);
    this.firebaseListenerCommon = AppInjector.get(FirebaseListenerCommon);

    this.eventEmitter
      .get(ActivitiesEnum.score)
      .subscribe(async () => {
        await this.dashboardSincronize();
      });

    this.eventEmitter
      .get(ActivitiesEnum.connection)
      .subscribe(async () => {
        await this.dashboardSincronize();
      });

    this.eventEmitter
      .get('ranking-updated-menu')
      .subscribe(async () => {
        await this.loadRanking();
      });

  }

  async ionDidOpen() {
    await this.loadRanking();
  }

  async loadRanking() {
    this.ranking = await this.rankingService.getUserRanking();
  }

  async dashboardSincronize() {

    try {
      await this.dashboardService.sincronize();
      this.dashboardDto = await this.dashboardService.get();

      this.eventEmitter.get('score-updated').emit(this.dashboardDto);
    }
    catch (e) {
      //todo log
    }
  }

  async ngOnInit() {

    this.dashboardDto = await this.dashboardService.get();

    this.ranking = await this.rankingService.getUserRanking();

    await this.angularFireAuth.useDeviceLanguage();

    this.firebaseListener.start();

    this.firebaseListenerCommon.start();

    this.angularFireAuth.onIdTokenChanged(async (u) => {
      if (u) {
        var token = await u.getIdToken();
        let tokenAuth: TokenAuth = new TokenAuth(token, u.refreshToken);
        this.authService.processToken(tokenAuth);
      }
    });

    this.displayControl.set(1);

    await this.pushNotificationService.init();

  }

  ngAfterViewInit() {
    this.platform.ready().then(async () => {
      await this.pushNotificationService.requestPermission();
    });
  }

  logoff() {
    this.authService.logoff().then(() => {
      this.firebaseListener.stop();
      this.firebaseListenerCommon.stop();
      this.routerRedirectService.redirect("login");
    });
  }
}
