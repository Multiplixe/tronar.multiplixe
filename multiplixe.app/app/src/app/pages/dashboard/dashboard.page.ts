import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';
import { RankingService } from 'src/app/services/ranking.service';
import { AppInjector } from 'src/app/app-injector';
import { RestrictPage } from '../restrict.page';
import { Dashboard as DashboardDto } from 'src/app/dtos/dashboard';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html'
})
export class DashboardPage extends RestrictPage
  implements OnInit {

  private dashboardService: DashboardService;
  private rankingService: RankingService;

  public dashboardDto: DashboardDto = DashboardDto.create();
  public ranking: number = 0;

  constructor() {
    super();
    this.dashboardService = AppInjector.get(DashboardService);
    this.rankingService = AppInjector.get(RankingService);

    super.addSubscription(this.eventEmitter
      .get('score-updated')
      .subscribe(() => {
        this.getScore();
      }));

  }

  async ngOnInit() {
    super.ngOnInit();
    await this.getScore();
  }

  connected() {
    this.redirectRestrict("minhas-conexoes")
  }

  async getScore() {

    try {

      await this.runLoading();

      this.dashboardDto = await this.dashboardService.get();

      this.ranking = await this.rankingService.getUserRanking();

      this.canShowContent();
    }
    catch (e) {

      console.log("getScore ", e)

      this.processError(e, "Ocorreu algum problema ao conectar no servidor. Por favor, tente novamente.", async () => { this.getScore() });
    }
    finally {
      await this.stopLoading();
    }
  }

}
