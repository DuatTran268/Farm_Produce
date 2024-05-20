import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { useSnackbar } from "notistack";
import { useSelector } from "react-redux";
import { Link, useParams } from "react-router-dom";
import { getFilterUser } from "../../../api/User";
import HeaderBtn from "../../../components/common/HeaderBtn";
import BtnSuccess from "../../../components/common/BtnSuccess";
import { faAdd, faEdit, faInfoCircle, faTrash } from "@fortawesome/free-solid-svg-icons";
import Loading from "../../../components/common/Loading";
import { Table } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BtnNextPage from "../../../components/common/BtnNextPage";
import Popup from "../../../components/common/Popup";
import UserFilter from "../../../components/admin/filter/UserFilter";

const AdUser = () => {
  const [getUser, setgetUser] = useState([]);
  const [refreshData, setRefreshData] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true),
    userFilter = useSelector((state) => state.userFilter);

  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [UserIdToDelete, setUserIdToDelete] = useState(null);

  let { id } = useParams,
    p = 1,
    ps = 5;
  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    document.title = "Quản lý Người dùng hệ thống";

    loadDepartment();
    async function loadDepartment() {
      function setData(props) {
        setgetUser(props.items);
        setMetadata(props.metadata);
      }
      getFilterUser(userFilter.name, ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [userFilter, ps, p, refreshData, pageNumber]);

  const handleDeleteUser = (id) => {
    setUserIdToDelete(id);
    setPopupMessage("Bạn có muốn xoá người dùng này?");
    setPopupVisible(true);
  };

  // const handleConfirmDelete = async () => {
  //   const response = await deletUser(UserIdToDelete);
  //   if (response) {
  //     enqueueSnackbar("Đã xoá thành công", {
  //       variant: "success",
  //     });
  //     setRefreshData((prev) => !prev);
  //   } else {
  //     enqueueSnackbar("Xoá thất bại", {
  //       variant: "error",
  //     });
  //   }
  //   setPopupVisible(false);
  // };

  const handleCancelDelete = () => {
    setPopupVisible(false);
  };

  return (
    <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Người dùng hệ thống</h3>
      </div>

      <HeaderBtn>
        {/* <BtnSuccess icon={faAdd} slug={"/admin/user/edit"} name="Thêm mới" /> */}
        <UserFilter/>
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>ID người dùng</th>
                <th>Họ tên</th>
                <th>Số điện thoại</th>
                <th>Email</th>
                <th>Thông tin</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getUser.length > 0 ? (
                getUser.map((item, index) => (
                  <tr key={index}>
                    <td>
                      <Link
                        // to={`/admin/user/edit/${item.id}`}
                        className="text-decoration-none text-success"
                      >
                        {item.id}
                      </Link>{" "}
                    </td>
                    <td>
                      {item.name || (
                        <div className="text-danger"> Không xác định </div>
                      )}
                    </td>
                    <td>
                      {item.phoneNumber ? (
                        <Link
                          to={`tel:${item.phoneNumber}`}
                          className="text-decoration-none "
                        >
                          {item.phoneNumber}
                        </Link>
                      ) : (
                        <div className="text-danger">Không xác định</div>
                      )}
                    </td>
                    <td>
                      {item.email ? (
                        <Link
                          to={`tel:${item.email}`}
                          className="text-decoration-none "
                        >
                          {item.email}
                        </Link>
                      ) : (
                        <div className="text-danger">Không xác định</div>
                      )}
                    </td>

                    <td className="text-center">
                      <Link
                        to={`/admin/user/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faInfoCircle} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={() => handleDeleteUser(item.id)}>
                        <FontAwesomeIcon icon={faTrash} color="red" />
                      </div>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={6}>
                    <h4 className="text-danger text-center">Không tìm thấy</h4>
                  </td>
                </tr>
              )}
            </tbody>
          </Table>
        )}
        <BtnNextPage metadata={metadata} onPageChange={updatePageNumber} />
      </div>

      {popupVisible && (
        <Popup
          message={popupMessage}
          onCancel={handleCancelDelete}
          // onConfirm={handleConfirmDelete}
        />
      )}
    </LayoutCommon>
  );
};
export default AdUser;
