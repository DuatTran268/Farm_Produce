import ReactApexChart from "react-apexcharts";

const ChartColumn = () => {
  const series = [
    {
      name: "Đã bán",
      data: [10, 2, 3],
    },
    {
      name: "Tồn kho",
      data: [10, 12, 31],
    },
  ];

  const options = {
    chart: {
      id: "bar-chart",
      stacked: true,
    },
    xaxis: {
      title: {
        text: "Biểu đồ bán hàng theo tháng",
        style: {
          fontSize: "16px",
          fontFamily: "Times New Roman",
        },
      },
    },
    title: {
      align: "center",
      style: {
        fontSize: "20px",
        fontFamily: "Times New Roman",
        marginTop: "4rem",
      },
    },
    legend: {
      position: "bottom",
    },
    colors: ["#00cc33", "#d41515"],
    plotOptions: {
      bar: {
        horizontal: false,
        dataLabels: {
          position: "center",
        },
      },
    },
  };

  return (
      <div className="control-pane">
        <div className="control-section">
          <ReactApexChart
            options={options}
            series={series}
            type="bar"
            height={480}
          />
        </div>
      </div>
  );
};

export default ChartColumn;
