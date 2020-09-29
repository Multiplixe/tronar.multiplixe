import { Level } from './level';
import { Pointing } from './pointing';
import { SocialMedia } from './social-media'

export class Score {
    public level: Level = Level.create();
    public pointing: Pointing = Pointing.create();
    public socialMedias: SocialMedia[] = [];

    static create() {
        return new Score();
    }    
}