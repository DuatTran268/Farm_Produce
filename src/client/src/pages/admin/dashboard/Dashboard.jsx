import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import ChartColumn from "../chart/Column";
import PieChart from "../chart/Pie";
import Statistics from "../chart/Statistics";
import { faCake, faCartArrowDown, faList, faProcedures, faUser } from "@fortawesome/free-solid-svg-icons";
import { getAllDashboard } from "../../../api/Dashboard";

const Dashboard = () => {
  const [dashboardItem, setDashboardItem] = useState({});

  useEffect(() => {
    document.title = "Trang quản trị hệ thống"
    getDashboard();
    async function getDashboard() {
      const response = await getAllDashboard();
      if (response) {
        setDashboardItem(response);
      } else {
        setDashboardItem({});
      }
    }
  }, []);

  return (
    <>
      <LayoutCommon>
        <div className="dashboard_chart ">
          <div className="dashboard_top col-12 row">
            <Statistics icon={faList} title={'Tổng số Danh mục'} value={dashboardItem.countCategory}/>
            <Statistics icon={faCake} title={'Tổng số Sản phẩm'} value={dashboardItem.countProduct}/>
            <Statistics icon={faCartArrowDown} title={'Tổng số Đơn hàng'} value={dashboardItem.countOrder}/>
            <Statistics icon={faUser} title={'Tổng số Người dùng'} value={dashboardItem.countUser}/>
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
