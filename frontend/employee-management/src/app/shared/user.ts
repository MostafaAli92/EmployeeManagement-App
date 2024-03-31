export class User {
  userName: string;
  password: string;
  firstName: string;
  lastName: string;
  email: string;
  roles: string[];

  constructor(userName: string, password: string, firstName: string='', lastName: string='', email: string='', roles: string[]=[]) {
    this.userName = userName;
    this.password = password;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.roles = roles;
  }
}
