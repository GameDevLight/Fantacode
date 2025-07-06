import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChartConfiguration, ChartType } from 'chart.js';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  chartData: ChartConfiguration<'bar'>['data'] = {
    labels: [],
    datasets: []
  };

  chartType: ChartType = 'bar';

  openCount = 0;
  closedCount = 0;
  progressCount = 0;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.http.get<any[]>('http://localhost:5270/api/dashboard/data').subscribe(data => {
      const labels = data.map(item => item.label);
      const values = data.map(item => item.value);

      this.chartData = {
        labels,
        datasets: [
          {
            label: 'Tickets',
            data: values,
            backgroundColor: ['#0d6efd', '#198754', '#ffc107'],
            borderRadius: 8
          }
        ]
      };

      this.openCount = data.find(d => d.label === 'Open')?.value || 0;
      this.closedCount = data.find(d => d.label === 'Closed')?.value || 0;
      this.progressCount = data.find(d => d.label === 'In Progress')?.value || 0;
    });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }
}
