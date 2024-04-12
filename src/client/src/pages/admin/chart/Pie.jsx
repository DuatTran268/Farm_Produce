import React from "react";

import ReactApexChart from "react-apexcharts";

function PieChart() {

 
  const series = [44, 55, 41];
  const options = {
    options: {
      chart: {
        width: 380,
        type: 'donut',
      },
      plotOptions: {
        pie: {
          startAngle: -90,
          endAngle: 270
        }
      },
      dataLabels: {
        enabled: false
      },
      fill: {
        type: 'gradient',
      },
      legend: {
        formatter: function(val, opts) {
          return val + " - " + opts.w.globals.series[opts.seriesIndex]
        }
      },
      title: {
        text: 'Biểu đồ doanh thu % theo tháng'
      },
      responsive: [{
        breakpoint: 480,
        options: {
          chart: {
            width: 300
          },
          legend: {
            position: 'bottom'
          }
        }
      }]
    },
  };

  return (
    <div className="pie-item m-5">
      <ReactApexChart
        options={options}
        series={series}
        type="donut"
        width={300}
        height={400}
      />
    </div>
  );
}
export default PieChart;