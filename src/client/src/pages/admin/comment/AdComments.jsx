import React, { useEffect, useState } from "react";
import { useSnackbar } from "notistack";
import { useSelector } from "react-redux";
import { Link, useParams } from "react-router-dom";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import HeaderBtn from "../../../components/common/HeaderBtn";
import BtnSuccess from "../../../components/common/BtnSuccess";
import CommentFilter from "../../../components/admin/filter/CommentFilter";
import { deleteComment, getFilterComment } from "../../../api/Comment";
import { faAdd, faEdit, faTrash, faStar } from "@fortawesome/free-solid-svg-icons";
import Loading from "../../../components/common/Loading";
import { Table } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BtnNextPage from "../../../components/common/BtnNextPage";
import { format } from "date-fns";
import Popup from "../../../components/common/Popup";

import { faStar as faStarRegular } from "@fortawesome/free-regular-svg-icons";

const AdComments = () => {
  const [getCommnent, setgetCommnent] = useState([]);
  const [reRender, setRender] = useState(false);
  const [refreshData, setRefreshData] = useState(false);

  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true),
    commentFilter = useSelector((state) => state.commentFilter);

  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [unitIdToDelete, setUnitIdToDelete] = useState(null);


  let { id } = useParams,
    p = 1,
    ps = 5;
  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    document.title = "Quản lý Commnet";

    loadDepartment();
    async function loadDepartment() {
      function setData(props) {
        setgetCommnent(props.items);
        setMetadata(props.metadata);
      }
      getFilterComment(commentFilter.name, ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [commentFilter, ps, p, refreshData, pageNumber]);


  const handleDelete = (id) => {
    setUnitIdToDelete(id);
    setPopupMessage("Bạn có muốn xoá category này?");
    setPopupVisible(true);
  };

  const handleConfirmDelete = async () => {
    const response = await deleteComment(unitIdToDelete);
    if (response) {
      enqueueSnackbar("Đã xoá thành công", {
        variant: "success",
      });
      setRender((prev) => !prev);
    } else {
      enqueueSnackbar("Xoá thất bại", {
        variant: "error",
      });
    }
    setPopupVisible(false);
  };

  const handleCancelDelete = () => {
    setPopupVisible(false);
  };


  const renderRatingStars = (rating) => {
    const stars = [];
    for (let i = 0; i < 5; i++) {
      if (i < rating) {
        stars.push(  <FontAwesomeIcon key={i} icon={faStar} className="text-warning"/>);
      } else {
        stars.push(<FontAwesomeIcon key={i} icon={faStarRegular} className="text-warning"/>);
      }
    }
    return stars;
  };



  return (
    <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Comment</h3>
      </div>

      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/comment/edit"} name="Thêm mới" />
        <CommentFilter />
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>Tên Người Bình Luận</th>
                <th>Số  sao đánh giá</th>
                <th>Ngày đánh giá</th>
                {/* <th>Trạng thái</th> */}
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getCommnent.length > 0 ? (
                getCommnent.map((item, index) => (
                  <tr key={index}>
                    <td>{item.name}</td>
                    <td>
                    {renderRatingStars(item.rating)}
                      </td>
                    <td>{format(new Date(item.created), "dd/MM/yyyy")}</td>
                    {/* <td>{item.status}</td> */}

                    <td className="text-center">
                      <Link
                        to={`/admin/comment/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faEdit} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={() => handleDelete(item.id)}>
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
          onConfirm={handleConfirmDelete}
        />
      )}
    </LayoutCommon>
  );
};
export default AdComments;
