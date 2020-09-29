import { BaseEnvelope } from "./base.envelope";
import { ErrorEnvelope } from "./error.envelope";

export class Envelope<T> extends BaseEnvelope {
  public item: T;
  public culture: string = "pt-BR";
  public success: boolean;
  public error: ErrorEnvelope;
}


