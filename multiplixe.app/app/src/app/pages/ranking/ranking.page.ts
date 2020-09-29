import { Component, OnInit } from '@angular/core';
import { RestrictPage } from '../restrict.page';
import { RankingService } from 'src/app/services/ranking.service';
import { AppInjector } from 'src/app/app-injector';
import { Ranking } from 'src/app/dtos/ranking/ranking';
import { AngularFireStorage } from '@angular/fire/storage';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.page.html'
})
export class RankingPage extends RestrictPage implements OnInit {

  private rankingService: RankingService;
  public ranking: Ranking = Ranking.create();
  private angularFireStorage: AngularFireStorage;

  constructor() {
    super();
    this.rankingService = AppInjector.get(RankingService)
    this.angularFireStorage = AppInjector.get(AngularFireStorage);
  }

  async ngOnInit() {
    super.ngOnInit();

    this.load();
  }

  async load() {

    try {

      await this.runLoading();

      this.ranking = await this.rankingService.get();

      this.eventEmitter
        .get('ranking-updated-menu')
        .emit();

      this.setAvatar();

      super.rankingChanged = false;

      this.canShowContent();
    }
    catch (e) {
      this.processError(e, "Ocorreu algum problema ao conectar no servidor. Por favor, tente novamente.", async () => { this.load() });
    }
    finally {
      await this.stopLoading();
    }
  }

  setAvatar() {

    this.ranking.placing.forEach(e => {

      this.angularFireStorage
        .ref("user-profile")
        .child("avatar")
        .child(e.user + '.jpg')
        .getDownloadURL()
        .toPromise()
        .then((i) => {
          e.imageUrl = i;
        })
        .catch(() => {
          e.imageUrl = '/assets/img/avatar/default.jpg';
        });

    });



  }


}
