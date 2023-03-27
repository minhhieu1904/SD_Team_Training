
export interface ChangePassword {
  account: string;
  old_password: string;
  new_password: string;
}


export interface ResultOfSave {
  results: string;
  message: string;
}
