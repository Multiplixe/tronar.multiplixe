import { BaseEnvelope } from './base.envelope';
import { EntryCodeEnum } from '../entries/entry.enum';

export class ErrorEnvelope extends BaseEnvelope  {
  public messages: string[] = [];
  public code: EntryCodeEnum = EntryCodeEnum.none;
}

