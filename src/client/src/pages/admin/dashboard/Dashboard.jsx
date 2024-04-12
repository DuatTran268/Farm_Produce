import React from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import ChartColumn from "../chart/Column";
import PieChart from "../chart/Pie";

const Dashboard = () => {
  return (
    <>
      <LayoutCommon>
        <div className="dashboard_chart row">
          <div className="dashboard_chart_col col-7">
            <ChartColumn/>
          </div>
          <div className="dashboard_chart_col col-4">
            <PieChart/>
          </div>

        </div>

      </LayoutCommon>
    </>
  );
};
export default Dashboard;
