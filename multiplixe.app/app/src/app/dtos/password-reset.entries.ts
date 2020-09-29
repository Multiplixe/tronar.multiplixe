import { BaseEntries } from './base-entries';
import { StringEntry } from '../entries/string.entry';

export class PasswordResetEntries extends BaseEntries {
    public password : StringEntry = StringEntry.Create();
    public mode : string = '';
    public oobCode : string = '';
    public apiKey : string = '';

    static Create(): PasswordResetEntries {
        return new PasswordResetEntries();
    }

    resetAll() {
        this.password.reset();
    }

}