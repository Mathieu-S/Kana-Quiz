import { Page } from "../utils";

class QuestionPage implements Page {
  Name = "Question";
  public OnInit(): void {}
}

export const Question = new QuestionPage();
