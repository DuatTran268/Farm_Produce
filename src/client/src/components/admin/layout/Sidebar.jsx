import React, { useState } from "react";
import {
  faArrowCircleUp,
  faCar,
  faDashboard,
  faEdit,
  faList,
  faMessage,
  faSignOut,
  faTicket,
  faUpDown,
  faUser,
  faUserCircle,
} from "@fortawesome/free-solid-svg-icons";
import SidebarCommon from "../common/SidebarCommon";

const Sidebar = () => {
  const [activeMenu, setActiveMenu] = useState();
  const handleMenuClick = (slug) => {
    setActiveMenu(slug);
  };

  return (
    <>
      <div className="sidebar">
        <ul>
          <div className="sidebar-main">
            <span className="sidebar-tile">Bảng điều khiển</span>
            <SidebarCommon
              slug="dashboard"
              icon={faDashboard}
              title="Dashboard"
              active={activeMenu === "dashboard"} // Kiểm tra xem menu này có được chọn không
              onClick={handleMenuClick}
            />
          </div>

          <div className="sidebar-main">
            <span className="sidebar-tile">Quản lý</span>
            <SidebarCommon
              slug="product"
              icon={faTicket}
              title="Sản phẩm"
              active={activeMenu === "product"}
              onClick={handleMenuClick}
            />
            <SidebarCommon
              slug="category"
              icon={faList}
              title="Danh mục"
              active={activeMenu === "category"}
              onClick={handleMenuClick}
            />
            <SidebarCommon
              slug="order"
              icon={faArrowCircleUp}
              title="Đơn hàng"
              active={activeMenu === "order"}
              onClick={handleMenuClick}
            />
            <SidebarCommon
              slug="deliver"
              icon={faCar}
              title="Vận chuyển"
              active={activeMenu === "deliver"}
              onClick={handleMenuClick}
            />
            <SidebarCommon
              slug="comment"
              icon={faMessage}
              title="Bình luận"
              active={activeMenu === "comment"}
              onClick={handleMenuClick}
            />
            <SidebarCommon
              slug="user"
              icon={faUser}
              title="Khách hàng"
              active={activeMenu === "user"}
              onClick={handleMenuClick}
            />
            <SidebarCommon
              slug="user"
              icon={faUserCircle}
              title="Người dùng"
              active={activeMenu === "user"}
              onClick={handleMenuClick}
            />
            <SidebarCommon
              slug="unit"
              icon={faList}
              title="Đơn vị"
              active={activeMenu === "unit"}
              onClick={handleMenuClick}
            />

            <SidebarCommon
              slug="discount"
              icon={faUpDown}
              title="Giảm giá"
              active={activeMenu === "discount"}
              onClick={handleMenuClick}
            />
          </div>

          <div className="sidebar-main">
            <span className="sidebar-tile">Cài đặt</span>
            <SidebarCommon
              slug="setting"
              icon={faEdit}
              title="Cài đặt"
              active={activeMenu === "setting"}
              onClick={handleMenuClick}
            />
            <SidebarCommon slug="" icon={faSignOut} title="Đăng xuất" />
          </div>
        </ul>
      </div>
    </>
  );
};
export default Sidebar;
