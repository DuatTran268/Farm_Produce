import React from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import ChartColumn from "../chart/Column";
import PieChart from "../chart/Pie";
import Statistics from "../chart/Statistics";
import { faUser } from "@fortawesome/free-solid-svg-icons";

const Dashboard = () => {
  return (
    <>
      <LayoutCommon>
        <div className="dashboard_chart ">
          <div className="dashboard_top col-12 row">
            <Statistics icon={faUser} title={'Tổng số khách hàng'} value={10000}/>
            <Statistics icon={faUser} title={'Tổng số khách hàng'} value={10000}/>
            <Statistics icon={faUser} title={'Tổng số khách hàng'} value={10000}/>
            <Statistics icon={faUser} title={'Tổng số khách hàng'} value={10000}/>
          </div>
          <div className="chart_dashboard row mt-3">
            <div className="dashboard_chart_col col-7 ">
              <ChartColumn/>
            </div>
            <div className="dashboard_chart_col col-4">
              <PieChart/>
              <PieChart/>
            </div>
          </div>

        </div>

      </LayoutCommon>
    </>
  );
};
export default Dashboard;
