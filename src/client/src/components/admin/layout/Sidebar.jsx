import React from "react";
import {
  faArrowCircleUp,
  faCar,
  faDashboard,
  faEdit,
  faList,
  faMessage,
  faSignOut,
  faTicket,
  faUser,
  faUserCircle,
} from "@fortawesome/free-solid-svg-icons";
import SidebarCommon from "../common/SidebarCommon";

const Sidebar = () => {
  return (
    <>
      <div className="sidebar">
        <ul>
          <div className="sidebar-main">
            <span className="sidebar-tile">Bảng điều khiển</span>
            <SidebarCommon slug="dashboard" icon={faDashboard} title="Dashboard"/>
          </div>

          <div className="sidebar-main">
            <span className="sidebar-tile">Quản lý</span>
            <SidebarCommon slug="product" icon={faTicket} title="Sản phẩm" />
            <SidebarCommon slug="category" icon={faList} title="Danh mục" />
            <SidebarCommon slug="order" icon={faArrowCircleUp} title="Đơn hàng" />
            <SidebarCommon slug="product" icon={faCar} title="Vận chuyển"/>
            <SidebarCommon slug="product" icon={faMessage} title="Bình luận" />
            <SidebarCommon slug="user" icon={faUser} title="Khách hàng" />
            <SidebarCommon slug="user" icon={faUserCircle} title="Người dùng" />
            <SidebarCommon slug="unit" icon={faList} title="Đơn vị" />

          </div>

          <div className="sidebar-main">
            <span className="sidebar-tile">Cài đặt</span>
            <SidebarCommon slug="product" icon={faEdit} title="Cài đặt" />
            <SidebarCommon slug="" icon={faSignOut} title="Đăng xuất" />
          </div>
        </ul>
      </div>
    </>
  );
};
export default Sidebar;
