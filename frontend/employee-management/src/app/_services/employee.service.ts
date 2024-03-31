import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../shared/employee';

const baseUrl = 'https://localhost:44389/api/Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http:HttpClient) { }

  getAllEmployees():Observable<Employee[]>{
    return this.http.get<Employee[]>(baseUrl);
  }

  updateEmployee(employee: Employee):Observable<any>{
    return this.http.put( baseUrl + '/' + employee.id, employee);
  }

  addEmployee(employee: Employee):Observable<any>{
    return this.http.post( baseUrl , employee);
  }

  removeEmployee(employeeId:number):Observable<any>{
    return this.http.delete(baseUrl + '/' + employeeId)
  }






}
