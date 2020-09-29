import { BaseService } from '../base.service';
import { IActivity } from './iactivity';
import { Score } from './score';
import { ActivitiesEnum } from 'src/app/enums/activities.enum';
import { Avatar } from './avata';
import { Connection } from './connection';

export class Factory extends BaseService {

  get(activityName: string) {

    let activity: IActivity;

    if (activityName == ActivitiesEnum.score) {
      activity = Score.create();
    }
    else if (activityName == ActivitiesEnum.avatar) {
      activity = Avatar.create();
    }
    else if (activityName == ActivitiesEnum.connection) {
      activity = Connection.create();
    }
    return activity;
  }
}