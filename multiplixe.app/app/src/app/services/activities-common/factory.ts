import { BaseService } from '../base.service';
import { ActivitiesCommonEnum } from 'src/app/enums/activities-common.enum';
import { IActivity } from './iactivity';
import { Ranking } from './ranking';

export class Factory extends BaseService {

  get(activityName: string) {

    let activity: IActivity;

    if (activityName == ActivitiesCommonEnum.ranking) {
      activity = Ranking.create();
    }

    return activity;
  }
}