import { BaseEntries } from './base-entries';
import { StringEntry } from '../entries/string.entry';

export class PasswordResetRequestEntries extends BaseEntries {
    public email : StringEntry = StringEntry.Create();

    static Create(): PasswordResetRequestEntries {
        return new PasswordResetRequestEntries();
    }

    resetAll() {
        this.email.reset();
    }

}
