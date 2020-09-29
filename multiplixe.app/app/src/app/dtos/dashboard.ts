import { Score } from './score/score.dto';

export class Dashboard {

    public hasConnection: boolean = false;
    public score: Score = new Score();

    static create() {
        return new Dashboard();
    }

}